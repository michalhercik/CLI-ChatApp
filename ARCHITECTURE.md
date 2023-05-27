## Server

The main class is a `Server` which is listenting on the socket and if a new
client is connected the server checks for a free slot in a pool with
`ChatClient` classes. If there is a free slot than the client is accepted and
all requests from the client now resolves class `ChatClient` in a separate
`Task`. First of all Handshake begins. Handshake means that the client sends
nickname and password for the server. If the password is correct client can
begin sending commands. Each command is handled by `MessageHandler` class which
determines which command implementing interface `ICommand` to invoke. Class
`Server` is also mediator for managing threads, which are represented by
`ChatThread` class. 

## Client

The main class is `Client`. After start `Client` sends handshake informations
to the server and if succusseful `Client` starts `Task` which reads command
line and `Task` that listents on the socket. Input from command line as well as
response from server handles class `MessageHandler`.

## Communication protocol

The communication is via socket based TCP connection. The information is
exchange always via class `Request` or `Response` as JSON. Class `Request` is
used for a client to send some request to a server. Class `Response` is used
for server to send response to a client request or to send message to the
client.
