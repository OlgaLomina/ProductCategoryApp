use AdventureWorks
go

DROP PROCEDURE dbo.GetProductSubcategoryJson;  
GO  

Create procedure dbo.GetProductSubcategoryJson
as
SELECT TOP (1000) 
		ProductSubcategoryID
      ,ProductCategoryID
      ,Name
      ,rowguid
      ,ModifiedDate
  FROM Production.ProductSubcategory
    order by 1
  for json path
 
  
  exec AdventureWorks.dbo.GetProductSubcategoryJson