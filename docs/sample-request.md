
## sample Query Request
```json
{
  "select": [
      "PersonName", "Department", "Salary", "Id"
  ],
  "where": {
   "field": "PersonName",
   "op": 0,
   "value": "Demo"
},
  "sort": [
    {
      "field": "PersonName",
      "direction": 1
    }, {
      "field": "Salary",
      "direction": 1
    }
  ],
  "page": {
    "pageNo": 2,
    "pageSize": 10
  }
}


{
  "select": [
      "PersonName", "Department", "Salary", "Id"
  ],
  "where": {
     "op": 0,
     "filters": [{
       "field": "PersonName",
       "op": 0,
       "value": "Demo"
   }],
  },
  "sort": [
    {
      "field": "PersonName",
      "direction": 1
    }, {
      "field": "Salary",
      "direction": 1
    }
  ],
  "page": {
    "pageNo": 2,
    "pageSize": 0
  }
}
```