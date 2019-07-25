use AdventureWorks
go

SELECT product.ProductID
      ,product.Name
      ,product.ProductNumber
      ,product.Color
      ,product.StandardCost
      ,product.ListPrice,
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
  ) as [Subcategory]
  FROM Production.Product product 
 where product.ProductID > 700
 order by 1
  for json path