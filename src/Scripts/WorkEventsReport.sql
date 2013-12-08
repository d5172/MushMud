SELECT
	Artist.Name, 
	ISNULL(p.Name, '(single)') as Album,
	w.Name as Title,  
	DomainEvent.EventDate,
	 DomainEvent.Class, 
	Person.Name
FROM Work w
LEFT JOIN Work p ON p.Id = w.ParentWorkId
INNER JOIN Artist ON Artist.Id = w.ArtistId
INNER JOIN DomainEvent ON DomainEvent.WorkId = w.Id
LEFT JOIN Person ON Person.Id = DomainEvent.PersonId
ORDER BY EventDate DESC