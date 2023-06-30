
DECLARE @cont INT = 1;

WHILE @cont < 5001
BEGIN
  insert into Animal (BreedId, StatusId, SexId, Name, Birthdate, Price) values (1,1,1,'Name of Animal ' + cast(@cont as varchar),GETDATE(),'1000');
  SET @cont = @cont + 1;
END;

--select count(*) from Animal;
--select * from animal
