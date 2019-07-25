use AdventureWorks
go

DROP PROCEDURE dbo.GetProductDescriptionHistoryJson;  
GO  

Create procedure dbo.GetProductDescriptionHistoryJson
as

SELECT product.ProductID
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
      ,product.ModifiedDate,
	  (
  select prodsub.ProductSubcategoryID as subcategID,
	  prodsub.Name as subcategName,
	  prodsub.ModifiedDate as subcategModiffiedDate,
	  prodsub.rowguid as subcategRowguid,
	  (
	  select prodcateg.ProductCategoryID as categID,
	  prodcateg.Name as categName,
	  prodcateg.ModifiedDate as categModiffiedDate,
	  prodcateg.rowguid as categRowguid
	  from Production.ProductCategory prodcateg
	  where prodsub.ProductCategoryID = prodcateg.ProductCategoryID for json path
	  ) as [Category]
  FROM Production.ProductSubcategory prodsub
  where product.ProductSubcategoryID = prodsub.ProductSubcategoryID for json path
  ) as [Subcategory],
  (
  select model.ModifiedDate,
	  model.Name,
	  model.ProductModelID,
	  model.rowguid
  FROM Production.ProductModel model
  where product.ProductModelID = model.ProductModelID for json path
  ) as [Model],
  (
  select cost_hist.EndDate,
  cost_hist.ModifiedDate,
  cost_hist.StandardCost,
  cost_hist.StartDate
  from Production.ProductCostHistory as cost_hist  
  where cost_hist.ProductID = product.ProductID for json path
  ) as [CostHistory]
  FROM Production.Product product 
 where product.ProductID > 800
 order by 1
  for json path
 
  
  
  exec AdventureWorks.dbo.GetProductDescriptionHistoryJson