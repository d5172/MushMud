---------------------------
-- Add the Comment table --
---------------------------
create table mc.Comment (
    Id UNIQUEIDENTIFIER not null,
   WorkId UNIQUEIDENTIFIER not null,
   PersonId UNIQUEIDENTIFIER not null,
   CommentText VARCHAR(1000) not null,
   DateEntered DATETIME not null,
   primary key (Id)
);
go
alter table mc.Comment 
    add constraint FK_Comment_Work 
    foreign key (WorkId) 
    references mc.Work;
go
alter table mc.Comment 
    add constraint FK_Comment_Person 
    foreign key (PersonId) 
    references mc.Person;
go