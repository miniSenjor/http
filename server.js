const express = require('express');
const app = express();
const port = 3000;

app.use(express.json());

app.get('/api/my', (req, res) => {
    res.json({ message: 'Hello from GET!' });
});

app.post('/api/my', (req, res) => {
    const data = req.body;
    if (!data) {
        return res.status(400).json({ error: 'No data provided' });
    }
    res.json({ message: 'Received data!', data });
});

app.listen(port, () => {
    console.log(`Server is running at http://localhost:${port}`);
});
