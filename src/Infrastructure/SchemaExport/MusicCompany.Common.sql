
    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'mc.[FKE0A654C13AC20257]') AND parent_object_id = OBJECT_ID('mc.TopLevelWorkSummaryView'))
alter table mc.TopLevelWorkSummaryView  drop constraint FKE0A654C13AC20257


    if exists (select * from dbo.sysobjects where id = object_id(N'mc.ArtistDetailView') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table mc.ArtistDetailView

    if exists (select * from dbo.sysobjects where id = object_id(N'mc.LicenseSummaryView') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table mc.LicenseSummaryView

    if exists (select * from dbo.sysobjects where id = object_id(N'mc.DomainEventView') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table mc.DomainEventView

    if exists (select * from dbo.sysobjects where id = object_id(N'mc.AudioTrackSummaryView') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table mc.AudioTrackSummaryView

    if exists (select * from dbo.sysobjects where id = object_id(N'mc.TopLevelWorkSummaryView') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table mc.TopLevelWorkSummaryView

    if exists (select * from dbo.sysobjects where id = object_id(N'mc.ImageDetailView') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table mc.ImageDetailView

    if exists (select * from dbo.sysobjects where id = object_id(N'mc.ArtistPersonSummaryView') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table mc.ArtistPersonSummaryView

    if exists (select * from dbo.sysobjects where id = object_id(N'mc.ArtistSummaryView') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table mc.ArtistSummaryView

    if exists (select * from dbo.sysobjects where id = object_id(N'mc.LicenseDetailView') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table mc.LicenseDetailView

    if exists (select * from dbo.sysobjects where id = object_id(N'mc.TagView') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table mc.TagView

    if exists (select * from dbo.sysobjects where id = object_id(N'mc.CommentView') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table mc.CommentView

    create table mc.ArtistDetailView (
        Identifier NVARCHAR(255) not null,
       Name NVARCHAR(255) null,
       Bio NVARCHAR(255) null,
       ProfileImageId UNIQUEIDENTIFIER null,
       primary key (Identifier)
    )

    create table mc.LicenseSummaryView (
        Identifier NVARCHAR(255) not null,
       Name NVARCHAR(255) null,
       ViewOrder INT null,
       primary key (Identifier)
    )

    create table mc.DomainEventView (
        Id UNIQUEIDENTIFIER not null,
       Username NVARCHAR(255) null,
       DomainEventType NVARCHAR(255) null,
       EventUsername NVARCHAR(255) null,
       Title NVARCHAR(255) null,
       WorkIdentifier NVARCHAR(255) null,
       WorkType NVARCHAR(255) null,
       CollectionTitle NVARCHAR(255) null,
       CollectionIdentifier NVARCHAR(255) null,
       ArtistName NVARCHAR(255) null,
       ArtistIdentifier NVARCHAR(255) null,
       EventDate DATETIME null,
       primary key (Id)
    )

    create table mc.AudioTrackSummaryView (
        Identifier NVARCHAR(255) not null,
       ArtistIdentifier NVARCHAR(255) null,
       CollectionIdentifier NVARCHAR(255) null,
       ViewOrder INT null,
       Title NVARCHAR(255) null,
       Description NVARCHAR(255) null,
       Tags NVARCHAR(255) null,
       BinaryFileId UNIQUEIDENTIFIER null,
       FileFormat NVARCHAR(255) null,
       AlternateFileFormat NVARCHAR(255) null,
       Seconds INT null,
       PlayCount INT null,
       DownloadCount INT null,
       primary key (Identifier)
    )

    create table mc.TopLevelWorkSummaryView (
        Id UNIQUEIDENTIFIER not null,
       Class NVARCHAR(255) not null,
       Identifier NVARCHAR(255) null,
       Title NVARCHAR(255) null,
       ArtistIdentifier NVARCHAR(255) null,
       ArtistName NVARCHAR(255) null,
       ArtistImageId UNIQUEIDENTIFIER null,
       Description NVARCHAR(255) null,
       ReleaseDate DATETIME null,
       LicenseName NVARCHAR(255) null,
       LicenseIdentifier NVARCHAR(255) null,
       LicenseUrl NVARCHAR(255) null,
       LicenseImageUrl NVARCHAR(255) null,
       Tags NVARCHAR(255) null,
       Seconds INT null,
       DownloadCount INT null,
       PlayCount INT null,
       Rank INT null,
       BinaryFileId UNIQUEIDENTIFIER null,
       FileFormat NVARCHAR(255) null,
       IsReleased BIT null,
       TrackCount INT null,
       AlternateFileFormat NVARCHAR(255) null,
       primary key (Id)
    )

    create table mc.ImageDetailView (
        Id UNIQUEIDENTIFIER not null,
       FileFormat NVARCHAR(255) null,
       Width INT null,
       OriginalFileName NVARCHAR(255) null,
       Data VARBINARY(8000) null,
       primary key (Id)
    )

    create table mc.ArtistPersonSummaryView (
        Identifier NVARCHAR(255) not null,
       Name NVARCHAR(255) null,
       ProfileImageId UNIQUEIDENTIFIER null,
       CollectionCount INT null,
       SingleCount INT null,
       Username NVARCHAR(255) null,
       primary key (Identifier)
    )

    create table mc.ArtistSummaryView (
        Identifier NVARCHAR(255) not null,
       Name NVARCHAR(255) null,
       ProfileImageId UNIQUEIDENTIFIER null,
       CollectionCount INT null,
       SingleCount INT null,
       primary key (Identifier)
    )

    create table mc.LicenseDetailView (
        Identifier NVARCHAR(255) not null,
       Name NVARCHAR(255) null,
       Description NVARCHAR(255) null,
       ViewOrder INT null,
       Url NVARCHAR(255) null,
       ImageUrl NVARCHAR(255) null,
       primary key (Identifier)
    )

    create table mc.TagView (
        Lemma NVARCHAR(255) not null,
       primary key (Lemma)
    )

    create table mc.CommentView (
        Id UNIQUEIDENTIFIER not null,
       WorkIdentifier NVARCHAR(255) null,
       ArtistIdentifier NVARCHAR(255) null,
       Username NVARCHAR(255) null,
       CommentText NVARCHAR(255) null,
       DateEntered DATETIME null,
       primary key (Id)
    )

    alter table mc.TopLevelWorkSummaryView 
        add constraint FKE0A654C13AC20257 
        foreign key (ArtistIdentifier) 
        references mc.ArtistDetailView
