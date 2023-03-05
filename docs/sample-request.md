
## sample Query Request
```json
{
  "select": [
      "PersonName", "Department", "Salary", "Id"
  ],
  "where": {
     "op": 0,
     "filterType": 0,
     "filters": [{
        "filterType": 1,
        "field": "PersonName",
        "op": 0,
        "value": "Demo"
      }]
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



{
  "select": [
      "PersonName", "Department", "Salary", "Id"
  ],
  "where": {
     "op": 1,
     "filterType": 0,
     "filters": [{
        "filterType": 1,
        "field": "PersonName",
        "op": 0,
        "value": "Demo1"
      }, {
        "filterType": 1,
        "field": "PersonName",
        "op": 0,
        "value": "Demo2"
      }]
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
