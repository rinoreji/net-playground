using System;
using System.Linq;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("ef core experiments!");

using var db = new AppDBContext();

// Note: This sample requires the database to be created before running.
Console.WriteLine($"Database path: {db.DbPath}.");

// Create
Console.WriteLine("Inserting a new request");
db.Add(new Request { Desc=Guid.NewGuid().ToString() });
db.Add(new Request { Desc=Guid.NewGuid().ToString() });
db.SaveChanges();

// Read
Console.WriteLine("Querying for a request");
int? req = db.Requests
    .Where(r=>r.CreatedBy.Id==null)
    .OrderBy(b => b.Id)
    // .Select(x=> $"{x.Id} : {x.Desc} : {x.CreatedBy.Id}");
    .Select(x=> x.CreatedBy?.Id).FirstOrDefault();
Console.WriteLine(req);