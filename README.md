# Minimal Api Exercise

TODO:
- Create a backend application that exposes an API where via HTTP GET it returns the data in file `data.json`
- Protect the API with a fixed token to be sent in an HTTP header, otherwise the endpoint(s) return 401 Unauthorized.
- Create a frontend page that displays a grid of cards or list of items, containing the main fields present in `data.json` file.
- On every card/item there should be a delete button that, when clicked, will delete the item, both on server-side and client-side.
- The grid/list should be responsive: it should be on 3 colums on large screens, and 1 column on mobile.
- Create a search bar somewhere at the top that allows textual filtering (server-side) of the data by title.

## Documentation

### Requirements

````
- dotnetcore 7
````

### Usefull tools

````
- chasrp-ls
- omnisharp-roslyn
````

### Commands

```sh
dotnet run  #to start the application
dotnet test #to run tests
```

### Demo

go to localhost:5000 after start the application
