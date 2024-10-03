const http = require("http");
const port = 3000;
const host = `127.0.0.1`;

http.createServer(function(request,response){
	response.write(`
		<!DOCTYPE html>
		<html lang="en">
		<head>
			<meta charset="UTF-8">
			<meta name="viewport" content="width=device-width, initial-scale=1.0">
			<title>NodeJS Server</title>
		</head>
		<body>
			<h1>Hello My Server</h1>
		</body>
		</html>
		`);
	response.end();
}).listen(port, host ,function() {
    console.log(`Сервер начал работать ${host} ${port}`);
});

