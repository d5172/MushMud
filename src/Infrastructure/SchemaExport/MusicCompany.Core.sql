
    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'mc.[FK_Person_BinaryFileInfoId]') AND parent_object_id = OBJECT_ID('mc.Person'))
alter table mc.Person  drop constraint FK_Person_BinaryFileInfoId


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'mc.[FK_BinaryFileInfo_BinaryFileData]') AND parent_object_id = OBJECT_ID('mc.BinaryFileInfo'))
alter table mc.BinaryFileInfo  drop constraint FK_BinaryFileInfo_BinaryFileData


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'mc.[FK_BinaryFileInfo_Parent]') AND parent_object_id = OBJECT_ID('mc.BinaryFileInfo'))
alter table mc.BinaryFileInfo  drop constraint FK_BinaryFileInfo_Parent


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'mc.[FK_ArtistPerson_Artist]') AND parent_object_id = OBJECT_ID('mc.ArtistPerson'))
alter table mc.ArtistPerson  drop constraint FK_ArtistPerson_Artist


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'mc.[FK_ArtistPerson_Person]') AND parent_object_id = OBJECT_ID('mc.ArtistPerson'))
alter table mc.ArtistPerson  drop constraint FK_ArtistPerson_Person


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'mc.[FK_Work_Artist]') AND parent_object_id = OBJECT_ID('mc.Work'))
alter table mc.Work  drop constraint FK_Work_Artist


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'mc.[FK_Work_Collection]') AND parent_object_id = OBJECT_ID('mc.Work'))
alter table mc.Work  drop constraint FK_Work_Collection


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'mc.[FK_Work_License]') AND parent_object_id = OBJECT_ID('mc.Work'))
alter table mc.Work  drop constraint FK_Work_License


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'mc.[FK_Work_BinaryFileInfoId]') AND parent_object_id = OBJECT_ID('mc.Work'))
alter table mc.Work  drop constraint FK_Work_BinaryFileInfoId


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'mc.[FK7CD15E539F2DF165]') AND parent_object_id = OBJECT_ID('mc.WorkTag'))
alter table mc.WorkTag  drop constraint FK7CD15E539F2DF165


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'mc.[FK7CD15E5356A8996B]') AND parent_object_id = OBJECT_ID('mc.WorkTag'))
alter table mc.WorkTag  drop constraint FK7CD15E5356A8996B


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'mc.[FK_DomainEventPerson]') AND parent_object_id = OBJECT_ID('mc.DomainEvent'))
alter table mc.DomainEvent  drop constraint FK_DomainEventPerson


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'mc.[FK_DomainEvent_Work]') AND parent_object_id = OBJECT_ID('mc.DomainEvent'))
alter table mc.DomainEvent  drop constraint FK_DomainEvent_Work


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'mc.[FK_Artist_BinaryFileInfoId]') AND parent_object_id = OBJECT_ID('mc.Artist'))
alter table mc.Artist  drop constraint FK_Artist_BinaryFileInfoId


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'mc.[FK_HistoricWorkLicense_Work]') AND parent_object_id = OBJECT_ID('mc.HistoricWorkLicense'))
alter table mc.HistoricWorkLicense  drop constraint FK_HistoricWorkLicense_Work


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'mc.[FK_HistoricWorkLicense_License]') AND parent_object_id = OBJECT_ID('mc.HistoricWorkLicense'))
alter table mc.HistoricWorkLicense  drop constraint FK_HistoricWorkLicense_License


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'mc.[FK_Comment_Work]') AND parent_object_id = OBJECT_ID('mc.Comment'))
alter table mc.Comment  drop constraint FK_Comment_Work


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'mc.[FK_Comment_Person]') AND parent_object_id = OBJECT_ID('mc.Comment'))
alter table mc.Comment  drop constraint FK_Comment_Person


    if exists (select * from dbo.sysobjects where id = object_id(N'mc.License') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table mc.License

    if exists (select * from dbo.sysobjects where id = object_id(N'mc.Person') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table mc.Person

    if exists (select * from dbo.sysobjects where id = object_id(N'mc.BinaryFileInfo') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table mc.BinaryFileInfo

    if exists (select * from dbo.sysobjects where id = object_id(N'mc.ArtistPerson') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table mc.ArtistPerson

    if exists (select * from dbo.sysobjects where id = object_id(N'mc.Work') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table mc.Work

    if exists (select * from dbo.sysobjects where id = object_id(N'mc.WorkTag') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table mc.WorkTag

    if exists (select * from dbo.sysobjects where id = object_id(N'mc.DomainEvent') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table mc.DomainEvent

    if exists (select * from dbo.sysobjects where id = object_id(N'mc.Tag') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table mc.Tag

    if exists (select * from dbo.sysobjects where id = object_id(N'mc.Artist') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table mc.Artist

    if exists (select * from dbo.sysobjects where id = object_id(N'mc.HistoricWorkLicense') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table mc.HistoricWorkLicense

    if exists (select * from dbo.sysobjects where id = object_id(N'mc.BinaryFileData') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table mc.BinaryFileData

    if exists (select * from dbo.sysobjects where id = object_id(N'mc.Comment') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table mc.Comment

    create table mc.License (
        Id UNIQUEIDENTIFIER not null,
       Version DATETIME not null,
       ViewOrder INT not null,
       Abbreviation VARCHAR(25) not null,
       Title VARCHAR(100) not null,
       Url VARCHAR(500) not null,
       ImageUrl VARCHAR(500) not null,
       Description VARCHAR(MAX) not null,
       primary key (Id),
      unique (Abbreviation),
      unique (Title),
      unique (Url)
    )

    create table mc.Person (
        Id UNIQUEIDENTIFIER not null,
       Version DATETIME not null,
       Username VARCHAR(50) not null,
       Name VARCHAR(100) not null,
       BinaryFileInfoId UNIQUEIDENTIFIER null,
       primary key (Id)
    )

    create table mc.BinaryFileInfo (
        Id UNIQUEIDENTIFIER not null,
       Class NVARCHAR(255) not null,
       OriginalFileName VARCHAR(1000) not null,
       FileFormat VARCHAR(50) not null,
       ByteCount BIGINT not null,
       BinaryFileDataId UNIQUEIDENTIFIER not null,
       Seconds INT null,
       ParentId UNIQUEIDENTIFIER null,
       HorizontalResolution INT null,
       VerticalResolution INT null,
       ColorMode VARCHAR(50) null,
       primary key (Id)
    )

    create table mc.ArtistPerson (
        Id UNIQUEIDENTIFIER not null,
       Version DATETIME not null,
       ArtistId UNIQUEIDENTIFIER not null,
       PersonId UNIQUEIDENTIFIER not null,
       primary key (Id),
      unique (ArtistId, PersonId)
    )

    create table mc.Work (
        Id UNIQUEIDENTIFIER not null,
       Class NVARCHAR(255) not null,
       Version DATETIME not null,
       WorkType VARCHAR(50) not null,
       Name VARCHAR(500) not null,
       Identifier VARCHAR(500) not null,
       ArtistId UNIQUEIDENTIFIER not null,
       ParentWorkId UNIQUEIDENTIFIER null,
       LicenseId UNIQUEIDENTIFIER null,
       DateLicensed DATETIME not null,
       ReleaseDate DATETIME not null,
       Description VARCHAR(MAX) null,
       ViewOrder INT not null,
       BinaryFileInfoId UNIQUEIDENTIFIER null,
       primary key (Id)
    )

    create table mc.WorkTag (
        WorkId UNIQUEIDENTIFIER not null,
       TagId UNIQUEIDENTIFIER not null
    )

    create table mc.DomainEvent (
        Id UNIQUEIDENTIFIER not null,
       Class NVARCHAR(255) not null,
       DomainEventType VARCHAR(50) not null,
       PersonId UNIQUEIDENTIFIER null,
       EventDate DATETIME not null,
       WorkId UNIQUEIDENTIFIER not null,
       primary key (Id)
    )

    create table mc.Tag (
        Id UNIQUEIDENTIFIER not null,
       Lemma VARCHAR(50) not null,
       primary key (Id),
      unique (Lemma)
    )

    create table mc.Artist (
        Id UNIQUEIDENTIFIER not null,
       Version DATETIME not null,
       Name VARCHAR(500) not null,
       Identifier VARCHAR(500) not null,
       Bio VARCHAR(MAX) not null,
       BinaryFileInfoId UNIQUEIDENTIFIER null,
       primary key (Id),
      unique (Name),
      unique (Identifier)
    )

    create table mc.HistoricWorkLicense (
        Id UNIQUEIDENTIFIER not null,
       StartDate DATETIME not null,
       EndDate DATETIME not null,
       WorkId UNIQUEIDENTIFIER not null,
       LicenseId UNIQUEIDENTIFIER not null,
       primary key (Id),
      unique (WorkId, LicenseId)
    )

    create table mc.BinaryFileData (
        Id UNIQUEIDENTIFIER not null,
       Data Image not null,
       primary key (Id)
    )

    create table mc.Comment (
        Id UNIQUEIDENTIFIER not null,
       WorkId UNIQUEIDENTIFIER not null,
       PersonId UNIQUEIDENTIFIER not null,
       CommentText VARCHAR(1000) not null,
       DateEntered DATETIME not null,
       primary key (Id)
    )

    alter table mc.Person 
        add constraint FK_Person_BinaryFileInfoId 
        foreign key (BinaryFileInfoId) 
        references mc.BinaryFileInfo

    alter table mc.BinaryFileInfo 
        add constraint FK_BinaryFileInfo_BinaryFileData 
        foreign key (BinaryFileDataId) 
        references mc.BinaryFileData

    alter table mc.BinaryFileInfo 
        add constraint FK_BinaryFileInfo_Parent 
        foreign key (ParentId) 
        references mc.BinaryFileInfo

    alter table mc.ArtistPerson 
        add constraint FK_ArtistPerson_Artist 
        foreign key (ArtistId) 
        references mc.Artist

    alter table mc.ArtistPerson 
        add constraint FK_ArtistPerson_Person 
        foreign key (PersonId) 
        references mc.Person

    alter table mc.Work 
        add constraint FK_Work_Artist 
        foreign key (ArtistId) 
        references mc.Artist

    alter table mc.Work 
        add constraint FK_Work_Collection 
        foreign key (ParentWorkId) 
        references mc.Work

    alter table mc.Work 
        add constraint FK_Work_License 
        foreign key (LicenseId) 
        references mc.License

    alter table mc.Work 
        add constraint FK_Work_BinaryFileInfoId 
        foreign key (BinaryFileInfoId) 
        references mc.BinaryFileInfo

    alter table mc.WorkTag 
        add constraint FK7CD15E539F2DF165 
        foreign key (TagId) 
        references mc.Tag

    alter table mc.WorkTag 
        add constraint FK7CD15E5356A8996B 
        foreign key (WorkId) 
        references mc.Work

    alter table mc.DomainEvent 
        add constraint FK_DomainEventPerson 
        foreign key (PersonId) 
        references mc.Person

    alter table mc.DomainEvent 
        add constraint FK_DomainEvent_Work 
        foreign key (WorkId) 
        references mc.Work

    alter table mc.Artist 
        add constraint FK_Artist_BinaryFileInfoId 
        foreign key (BinaryFileInfoId) 
        references mc.BinaryFileInfo

    alter table mc.HistoricWorkLicense 
        add constraint FK_HistoricWorkLicense_Work 
        foreign key (WorkId) 
        references mc.Work

    alter table mc.HistoricWorkLicense 
        add constraint FK_HistoricWorkLicense_License 
        foreign key (LicenseId) 
        references mc.License

    alter table mc.Comment 
        add constraint FK_Comment_Work 
        foreign key (WorkId) 
        references mc.Work

    alter table mc.Comment 
        add constraint FK_Comment_Person 
        foreign key (PersonId) 
        references mc.Person
