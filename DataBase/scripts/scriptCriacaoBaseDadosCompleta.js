use TarefasDB;
db.createUser(
  {
    user: "tarefas_usr",
    pwd: "1234",
    roles: [ { role: "readWrite", db: "TarefasDB" } ]
  }
);

db.createCollection("Categorias", {
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

db.getCollection("Categorias").createIndex({ "Nome": 1 });

db.getCollection("Categorias").insertMany([{Nome: "Diárias"}]);
db.getCollection("Categorias").insertMany([{Nome: "Escolares"}]);
db.getCollection("Categorias").insertMany([{Nome: "Cuidados Pessoais"}]);


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