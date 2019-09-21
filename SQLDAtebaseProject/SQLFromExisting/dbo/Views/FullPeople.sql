CREATE VIEW [dbo].[FullPeople]
	AS
	SELECT p.* , [a].[StreetAddress], [a].[City], [a].[ZipCode]
	FROM Person p
	left join Address a on p.Id = a.PersonId