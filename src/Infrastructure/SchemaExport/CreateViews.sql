
-------------------------
-- ConcatenateWorkTags --
-------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[mc].[ConcatenateWorkTags]'))
DROP FUNCTION [mc].[ConcatenateWorkTags]
GO
CREATE FUNCTION [mc].[ConcatenateWorkTags]
(
	@WorkId UNIQUEIDENTIFIER
)
RETURNS VARCHAR(1000)
AS
BEGIN
	DECLARE @Tags VARCHAR(1000);
	SELECT 
		@Tags = ISNULL(@Tags + ', ', '') + Tag.Lemma 
	FROM
		[mc].WorkTag
		INNER JOIN [mc].Tag ON Tag.Id = WorkTag.TagId
	WHERE 
		WorkTag.WorkId = @WorkId;

	RETURN ISNULL(@Tags, '');
END
GO

-----------------------
-- ArtistSummaryView --
-----------------------
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[mc].[ArtistSummaryView]'))
DROP VIEW [mc].[ArtistSummaryView]
GO
CREATE VIEW [mc].[ArtistSummaryView] AS
SELECT
	Artist.Identifier,
	Artist.Name, 
	Artist.BinaryFileInfoId as ProfileImageId,
	(SELECT COUNT(Id) FROM [mc].Work WHERE WorkType = 'Collection' AND ArtistId = Artist.Id)  AS CollectionCount,
	(SELECT COUNT(Id) FROM [mc].Work WHERE WorkType != 'Collection' AND ParentWorkId IS NULL AND ArtistId = Artist.Id) AS SingleCount
FROM
	[mc].Artist
GROUP BY
	Artist.Id,
	Artist.Identifier,
	Artist.Name, 
	Artist.BinaryFileInfoId
GO

-----------------------------
-- ArtistPersonSummaryView --
-----------------------------
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[mc].[ArtistPersonSummaryView]'))
DROP VIEW [mc].[ArtistPersonSummaryView]
GO
CREATE VIEW [mc].[ArtistPersonSummaryView] AS
SELECT
	Artist.Identifier,
	Artist.Name,
	Artist.BinaryFileInfoId as ProfileImageId,
	Person.Username,
	(SELECT COUNT(Id) FROM [mc].Work WHERE WorkType = 'Collection' AND ArtistId = Artist.Id)  AS CollectionCount,
	(SELECT COUNT(Id) FROM [mc].Work WHERE WorkType != 'Collection' AND ParentWorkId IS NULL AND ArtistId = Artist.Id) AS SingleCount

FROM
	Artist
	INNER JOIN [mc].ArtistPerson ON ArtistPerson.ArtistId = Artist.Id
	INNER JOIN [mc].Person ON Person.Id = ArtistPerson.PersonId
GROUP BY
	Artist.Id,
	Artist.Identifier,
	Artist.Name, 
	Artist.BinaryFileInfoId,
	Person.Username
GO

-----------------------
-- ArtistDetailView --
-----------------------
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[mc].[ArtistDetailView]'))
DROP VIEW [mc].[ArtistDetailView]
GO
CREATE VIEW [mc].[ArtistDetailView] AS
SELECT
	Artist.Identifier,
	Artist.Name, 
	Artist.BinaryFileInfoId as ProfileImageId,
	Artist.Bio
FROM
	[mc].Artist
GO

---------------------
-- ImageDetailView --
---------------------
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[mc].[ImageDetailView]'))
DROP VIEW [mc].[ImageDetailView]
GO
CREATE VIEW [mc].[ImageDetailView] AS
SELECT
	BinaryFileInfo.Id,
	BinaryFileInfo.FileFormat, 
	BinaryFileInfo.HorizontalResolution AS Width,
	BinaryFileInfo.OriginalFileName,
	BinaryFileData.Data
FROM
	[mc].BinaryFileInfo
	INNER JOIN [mc].BinaryFileData ON BinaryFileData.Id = BinaryFileInfo.BinaryFileDataId
WHERE
	BinaryFileInfo.Class = 'ImageFileInfo'
GO

---------------------------
-- AudioTrackSummaryView --
---------------------------
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[mc].[AudioTrackSummaryView]'))
DROP VIEW [mc].[AudioTrackSummaryView]
GO
CREATE VIEW [mc].[AudioTrackSummaryView] AS
SELECT
	Work.ViewOrder,
	Work.Identifier,
	Work.Name AS Title,
	Artist.Identifier AS ArtistIdentifier,
	ParentWork.Identifier AS CollectionIdentifier,
	Work.Description,
	mc.ConcatenateWorkTags(Work.Id) AS Tags,
	BinaryFileInfo.Seconds,
	BinaryFileInfo.FileFormat,
	BinaryFileInfo.Id AS BinaryFileId,
	altFile.FileFormat AS AlternateFileFormat,
	(SELECT COUNT(Id) FROM [mc].DomainEvent WHERE WorkId= Work.Id AND Class = 'Download')  AS DownloadCount,
	(SELECT COUNT(Id) FROM [mc].DomainEvent WHERE WorkId = Work.Id AND Class = 'Play') AS PlayCount
FROM
	[mc].Work
	INNER JOIN [mc].Artist ON Artist.Id = Work.ArtistId
	INNER JOIN [mc].BinaryFileInfo ON BinaryFileInfo.Id = Work.BinaryFileInfoId
	LEFT OUTER JOIN [mc].BinaryFileInfo altFile ON altFile.ParentId = BinaryFileInfo.Id
	INNER JOIN [mc].Work ParentWork ON ParentWork.Id = Work.ParentWorkId
WHERE
	Work.Class = 'AudioWork'
GROUP BY
	Work.Id,
	Work.ViewOrder,
	Work.Identifier,
	Work.Name,
	Artist.Identifier,
	ParentWork.Identifier,
	Work.Description,
	mc.ConcatenateWorkTags(Work.Id),
	BinaryFileInfo.Seconds,
	BinaryFileInfo.FileFormat,
	BinaryFileInfo.Id,
	altFile.FileFormat
	
GO

-----------------------------
-- AudioSingleSummaryView --
----------------------------
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[mc].[AudioSingleSummaryView]'))
DROP VIEW [mc].[AudioSingleSummaryView]
GO
CREATE VIEW [mc].[AudioSingleSummaryView] AS
SELECT
	Work.Id,
	Work.Class,
	Work.Identifier,
	Work.Name AS Title,
	Artist.Identifier AS ArtistIdentifier,
	Artist.Name AS ArtistName,
	Artist.BinaryFileInfoId AS ArtistImageId,
	Work.Description,
	Work.ReleaseDate,
	BinaryFileInfo.Seconds,
	BinaryFileInfo.Id AS BinaryFileId,
	BinaryFileInfo.FileFormat,
	altFile.FileFormat AS AlternateFileFormat,
	License.Title AS LicenseName,
	License.Abbreviation AS LicenseIdentifier,
	License.Url AS LicenseUrl,
	License.ImageUrl AS LicenseImageUrl,
	mc.ConcatenateWorkTags(Work.Id) AS Tags,
	1 AS TrackCount,
	CASE WHEN Work.ReleaseDate <= GETDATE() THEN 1 ELSE 0 END AS IsReleased,
	(SELECT COUNT(Id) FROM DomainEvent WHERE WorkId= Work.Id AND Class = 'Download')  AS DownloadCount,
	(SELECT COUNT(Id) FROM DomainEvent WHERE WorkId = Work.Id AND Class = 'Play') AS PlayCount
FROM
	[mc].Work
	INNER JOIN [mc].Artist ON Artist.Id = Work.ArtistId
	INNER JOIN [mc].BinaryFileInfo ON BinaryFileInfo.Id = Work.BinaryFileInfoId
	LEFT OUTER JOIN [mc].BinaryFileInfo altFile ON altFile.ParentId = BinaryFileInfo.Id
	INNER JOIN [mc].License ON License.Id = Work.LicenseId
WHERE
	Work.ParentWorkId IS NULL
	AND
	Work.Class = 'AudioWork'
GROUP BY
	Work.Id,
	Work.Class,
	Work.Identifier,
	Work.Name,
	Artist.Identifier,
	Artist.Name,
	Artist.BinaryFileInfoId,
	Work.Description,
	Work.ReleaseDate,
	BinaryFileInfo.Seconds,
	BinaryFileInfo.FileFormat,
	BinaryFileInfo.Id,
	altFile.FileFormat,
	License.Title,
	License.Abbreviation,
	License.Url,
	License.ImageUrl,
	mc.ConcatenateWorkTags(Work.Id)
	
GO

-----------------------------
-- CollectionSummaryView --
----------------------------
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[mc].[CollectionSummaryView]'))
DROP VIEW [mc].[CollectionSummaryView]
GO
CREATE VIEW [mc].[CollectionSummaryView] AS
SELECT
	Work.Id,
	Work.Class,
	Work.Identifier,
	Work.Name AS Title,
	Artist.Identifier AS ArtistIdentifier,
	Artist.Name AS ArtistName,
	Artist.BinaryFileInfoId AS ArtistImageId,
	Work.Description,
	Work.ReleaseDate,
	SUM(ISNULL(AudioTrackSummaryView.Seconds, 0)) AS Seconds,
	BinaryFileInfo.Id AS BinaryFileId,
	BinaryFileInfo.FileFormat,
	NULL AS AlternateFileFormat,
	License.Title AS LicenseName,
	License.Abbreviation AS LicenseIdentifier,
	License.Url AS LicenseUrl,
	License.ImageUrl AS LicenseImageUrl,
	mc.ConcatenateWorkTags(Work.Id) AS Tags,
	CASE WHEN Work.ReleaseDate <= GETDATE() AND COUNT(AudioTrackSummaryView.Identifier) > 0 THEN 1 ELSE 0 END AS IsReleased,
	COUNT(AudioTrackSummaryView.Identifier) AS TrackCount,
	((SELECT COUNT(Id) FROM [mc].DomainEvent WHERE WorkId= Work.Id AND Class = 'Download') 
		+ SUM(ISNULL(AudioTrackSummaryView.DownloadCount, 0)))  AS DownloadCount,
	((SELECT COUNT(Id) FROM [mc].DomainEvent WHERE WorkId = Work.Id AND Class = 'Play')
		+ SUM(ISNULL(AudioTrackSummaryView.PlayCount, 0))) AS PlayCount
FROM
	[mc].Work
	INNER JOIN [mc].Artist ON Artist.Id = Work.ArtistId
	LEFT OUTER JOIN [mc].BinaryFileInfo ON BinaryFileInfo.Id = Work.BinaryFileInfoId
	INNER JOIN [mc].License ON License.Id = Work.LicenseId
	LEFT OUTER JOIN [mc].AudioTrackSummaryView ON AudioTrackSummaryView.CollectionIdentifier = Work.Identifier
WHERE
	Work.Class = 'CollectionWork'
GROUP BY
	Work.Id,
	Work.Class,
	Work.Identifier,
	Work.Name,
	Artist.Identifier,
	Artist.Name,
	Artist.BinaryFileInfoId,
	Work.Description,
	Work.ReleaseDate,
	BinaryFileInfo.Id,
	BinaryFileInfo.FileFormat,
	License.Title,
	License.Abbreviation,
	License.Url,
	License.ImageUrl,
	mc.ConcatenateWorkTags(Work.Id)
	
GO

-----------------
-- CommentView --
-----------------
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[mc].[CommentView]'))
DROP VIEW [mc].[CommentView]
GO
CREATE VIEW [mc].[CommentView] AS
SELECT
	[mc].Comment.Id,
	[mc].Comment.WorkId,
	[mc].Person.Username,
	[mc].Comment.CommentText,
	[mc].Comment.DateEntered
FROM
	[mc].Comment
	INNER JOIN [mc].Person ON [mc].Person.Id = [mc].Comment.PersonId
GO

----------------------
-- PersonDetailView --
----------------------
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[mc].[PersonDetailView]'))
DROP VIEW [mc].PersonDetailView
GO
CREATE VIEW [mc].PersonDetailView AS
SELECT
	[mc].Person.Username,
	[mc].Person.Name,
	[mc].Person.BinaryFileInfoId AS ProfileImageId
FROM
	[mc].Person
GO

-----------------------------
-- TopLevelWorkSummaryView --
-----------------------------
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[mc].[TopLevelWorkSummaryView]'))
DROP VIEW [mc].[TopLevelWorkSummaryView]
GO
CREATE VIEW [mc].[TopLevelWorkSummaryView] AS
SELECT
	Id,
	Class,
	Identifier,
	Title,
	ArtistIdentifier,
	ArtistName,
	ArtistImageId,
	Description,
	ReleaseDate,
	BinaryFileId,
	FileFormat,
	AlternateFileFormat,
	LicenseName,
	LicenseIdentifier,
	LicenseUrl,
	LicenseImageUrl,
	Tags,
	Seconds,
	TrackCount,
	DownloadCount,
	PlayCount,
	IsReleased,
	RANK() OVER (ORDER BY (DownloadCount + PlayCount) DESC) AS Rank
FROM(
	SELECT * FROM [mc].CollectionSummaryView
	UNION
	SELECT * FROM [mc].AudioSingleSummaryView) w
GROUP BY
	Id,
	Class,
	Identifier,
	Title,
	ArtistIdentifier,
	ArtistName,
	ArtistImageId,
	Description,
	ReleaseDate,
	Seconds,
	BinaryFileId,
	FileFormat,
	AlternateFileFormat,
	LicenseName,
	LicenseIdentifier,
	LicenseUrl,
	LicenseImageUrl,
	Tags,
	TrackCount,
	DownloadCount,
	PlayCount,
	IsReleased
GO

-----------------------------
-- TagView --
-----------------------------
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[mc].[TagView]'))
DROP VIEW [mc].[TagView]
GO
CREATE VIEW [mc].[TagView] AS
SELECT Lemma FROM [mc].Tag
GO

------------------------
-- LicenseSummaryView --
------------------------
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[mc].[LicenseSummaryView]'))
DROP VIEW [mc].[LicenseSummaryView]
GO
CREATE VIEW [mc].[LicenseSummaryView] AS
SELECT
	Abbreviation AS Identifier,	
	Title AS Name,
	ViewOrder
FROM
	[mc].License
GO

-----------------------
-- LicenseDetailView --
-----------------------
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[mc].[LicenseDetailView]'))
DROP VIEW [mc].[LicenseDetailView]
GO
CREATE VIEW [mc].[LicenseDetailView] AS
SELECT
	Abbreviation AS Identifier,	
	Title AS Name,
	Description,
	ViewOrder,
	Url,
	ImageUrl
FROM
	[mc].License
GO

---------------------
-- DomainEventView --
---------------------
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[mc].[DomainEventView]'))
DROP VIEW [mc].[DomainEventView]
GO
CREATE VIEW [mc].[DomainEventView] AS
SELECT
	DomainEvent.Id,
	OwningPerson.Username,
	DomainEvent.DomainEventType,
	EventPerson.Username AS EventUsername,
	Work.[Name] AS Title,
	Work.Identifier AS WorkIdentifier,
	Work.WorkType,
	ParentWork.[Name] AS CollectionTitle,
	ParentWork.Identifier AS CollectionIdentifier,
	Artist.[Name] AS ArtistName,
	Artist.Identifier AS ArtistIdentifier,
	DomainEvent.EventDate
FROM
	[mc].DomainEvent
	LEFT OUTER JOIN [mc].Person EventPerson ON EventPerson.Id = DomainEvent.PersonId
	INNER JOIN [mc].Work ON Work.Id = DomainEvent.WorkId
	LEFT OUTER JOIN [mc].Work ParentWork ON ParentWork.Id = Work.ParentWorkId
	INNER JOIN [mc]. Artist ON Artist.Id = Work.ArtistId
	INNER JOIN [mc].ArtistPerson ON ArtistPerson.ArtistId = Artist.Id
	INNER JOIN [mc].Person OwningPerson ON OwningPerson.Id = ArtistPerson.PersonId
GO

/*
SELECT * FROM [TopLevelWorkSummaryView]
SELECT * FROM AudioTrackSummaryView
SELECT * FROM AudioSingleSummaryView

SELECT * FROM ArtistPersonSummaryView
SELECT * FROM LicenseSummaryView
SELECT * FROM LicenseDetailView

SELECT * FROM ArtistSummaryView
*/