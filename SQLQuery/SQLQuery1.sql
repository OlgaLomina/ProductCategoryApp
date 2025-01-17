use AdventureWorks
go

DROP PROCEDURE dbo.GetProductionProductJson;  
GO  

Create procedure dbo.GetProductionProductJson
as

SELECT TOP (1000) 
	   product.ProductID
      ,product.Name
      ,product.ProductNumber
      ,product.MakeFlag
      ,product.FinishedGoodsFlag
      ,product.Color
      ,product.SafetyStockLevel
      ,product.ReorderPoint
      ,product.StandardCost
      ,product.ListPrice
      ,product.Size
      ,product.SizeUnitMeasureCode
      ,product.WeightUnitMeasureCode
      ,product.Weight
      ,product.DaysToManufacture
      ,product.ProductLine
      ,product.Class
      ,product.Style
      ,product.ProductModelID
      ,product.SellStartDate
      ,product.SellEndDate
      ,product.DiscontinuedDate
      ,product.rowguid
      ,product.ModifiedDate
FROM Production.Product product 
 where product.ProductID > 900
 order by 1
 for json path
 
  
  exec AdventureWorks.dbo.GetProductionProductJson