use TarefasDB;
db.createUser(
  {
    user: "tarefas_usr",
    pwd: "1234",
    roles: [ { role: "readWrite", db: "FluxoCaixa" } ]
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

db.getCollection("Categoria").createIndex({ "Nome": 1 });

db.Despesas.insertMany([{Nome: "Diárias"}]);
db.Despesas.insertMany([{Nome: "Escolares"}]);
db.Despesas.insertMany([{Nome: "Cuidados Pessoais"}]);


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