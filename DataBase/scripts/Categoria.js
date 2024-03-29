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


