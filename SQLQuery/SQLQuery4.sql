use AdventureWorks
go

DROP PROCEDURE dbo.GetProductCategoryJson;  
GO  

Create procedure dbo.GetProductCategoryJson
as
SELECT TOP (1000) 
		ProductCategoryID
      ,Name
      ,rowguid
      ,ModifiedDate
  FROM Production.ProductCategory
  order by 1
  for json path
 
  
  exec AdventureWorks.dbo.GetProductCategoryJson