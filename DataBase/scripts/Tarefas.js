db.createCollection("Tarefas", {
  validator: {
    bsonType: "object",
    required: ["Nome"],
    properties: {
      Nome: {
        bsonType: "string"
      }
    }
  },
  validationLevel: "moderate",
  validationAction: "warn"
});

db.getCollection("Tarefas").createIndex({ "DataInicio": 1 });
db.getCollection("Tarefas").createIndex({ "DataFinal": 1 });
db.getCollection("Tarefas").createIndex({ "Status": 1 });
db.getCollection("Tarefas").createIndex({ "CategoriaId": 1 });
db.getCollection("Tarefas").createIndex({ "Usuario": 1 });