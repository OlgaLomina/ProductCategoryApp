use AdventureWorks
go

Create procedure dbo.getpersonjson
as

select top 10 BusinessEntityID,Title,FirstName,MiddleName,LastName
,(
  select pt.Name as type, phone.PhoneNumber as number
  FROM Person.PersonPhone phone
  inner join Person.PhoneNumberType pt
  on phone.PhoneNumberTypeID = pt.PhoneNumberTypeID
  where phone.BusinessEntityID = p.BusinessEntityID for json path
  ) as [Phone]
from Person.Person p
for json path

exec AdventureWorks.dbo.getpersonjson
