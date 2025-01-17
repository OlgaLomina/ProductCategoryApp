use AdventureWorks
go
/****** Script for SelectTopNRows command from SSMS  ******/
SELECT product.ProductID
      ,product.ProductSubcategoryID
      ,product.ProductModelID
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
      ,product.SellStartDate
      ,product.SellEndDate
      ,product.DiscontinuedDate
      ,product.rowguid
      ,product.ModifiedDate
  FROM  AdventureWorks.Production.Product product