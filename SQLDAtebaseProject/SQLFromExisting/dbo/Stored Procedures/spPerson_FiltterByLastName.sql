CREATE PROCEDURE [dbo].[spPerson_FiltterByLastName]
	@LastName nvarchar(50)
	
AS
begin
	SELECT *
	From Person
	Where LastName = @LastName
end