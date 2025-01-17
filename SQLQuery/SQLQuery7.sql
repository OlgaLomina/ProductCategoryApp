USE AdventureWorks
GO
/****** Object:  StoredProcedure [dbo].[getpersonjson]    Script Date: 5/21/2019 11:15:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--use AdventureWorks;
ALTER procedure dbo.getpersonjson
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

exec dbo.getpersonjson
