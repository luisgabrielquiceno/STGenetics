insert into Breed (Name) values ('Angus');
insert into Breed (Name) values ('Brangus');
insert into Breed (Name) values ('Other');

insert into Sex (Name) values ('Male');
insert into Sex (Name) values ('Female');

insert into Status(Name) values ('Active');
insert into Status (Name) values ('Inactive');


insert into Animal (BreedId, StatusId, SexId, Name, Birthdate, Price) values (1,1,1,'Name of Animal 1',GETDATE(),'1000');

Update Animal set Name = 'Name of Animal 1 of 1 ' where AnimalId = 1;

Delete from Animal where AnimalId = 1;

Select * from Animal;



