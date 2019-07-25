# ProductCategoryApp
1. Create a procedure that convert data from SQL to JSON
2. Export data using bcp into JSON document
3. Run cmd with admin and run mongod.exe
4. Because we have file.json, so we run mongoimport.exe (files json, csv to mongoDB)

commands
----------
>mongo
>show dbs
>use nameOfDatabase
>show collections
>db.Person.find()
>db.Person.find().pretty()
>quit()      >db.Logout()


A selection from two collections
---------------------------------
>db.Student.aggregate([
  {
    $lookup:
    {
      from: "Dept",
      localField: "Department_ID",
      foreignField: "Deptno",
      as: "Common"
    }
  }
])
