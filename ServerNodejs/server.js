const express = require('express');
const path = require('path');

const app = express();
const PORT = process.env.PORT || 3000;

// Servir archivos estáticos desde la carpeta del proyecto angular
app.use(express.static(path.join(__dirname, 'GestionPedido/dist/gestion-pedido/browser')));

// Maneja todas las rutas y redirigir a 'index.html'
app.get('*', (req, res) => {
  res.sendFile(path.join(__dirname, 'GestionPedido/dist/gestion-pedido/browser/index.html'));
});

app.listen(PORT, () => {
  console.log(`Servidor corriendo en http://localhost:${PORT}`);
});
