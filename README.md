# Chat App

 - Simple CLI chatting client-server app.
 - Communication is split into public or private threads.
 - Up to 100 clients and 20 threads.
 - Possible to setup a password for server.

## Server usage

Server can be setup via command-line options.
 - `--ipAdress [value]` or `--ip [value]` is for setting IP address for the
   server (required).
 - `--socket [value]` or `-s [value]` is for setting socket for the server
   (default is 5555).
 - `--password [value]` or `-p [value]` is for setting password for the server
   (default is no password).

For example to run server on a IP address `127.0.0.1` and socket `3434` with a
password `BOBO` you are expected to be inside `Server` project and run the
following command.

`dotnet run -- --ip 127.0.0.1 -s 3434 -p BOBO`

or

`dotnet run -- --ipAddress 127.0.0.1 --socket 3434 --password BOBO`

Or any other combination of aliases. After the start, server will show log
messages for the following events.
 - Start of the server.
 - New client connection.
 - Result of clients exchange of a informations (pasword, nick) with the server
   (aka handshake).
 - Client disconnection.
 - Creation of new thread (public or private).


## Client usage

Client can be setup via command-line options.
 - `--ipAdress [value]` or `--ip [value]` is IP address of the
   server (required).
 - `--socket [value]` or `-s [value]` is socket of the server
   (default is 5555).
 - `--password [value]` or `-p [value]` is password of the server
   (default is no password).
 - `--nickname [value]` or `-n [value]` is for setting nickname for the server
   (optional).

For example to run client that connects to a server on a IP address `127.0.0.1`
and socket `3434` with a password `BOBO` and nickname `Cucumber` you are
expected to be inside `Client` project and run the following command.

`dotnet run -- --ip 127.0.0.1 -s 3434 -p BOBO -n Cucumber`

or 

`dotnet run -- --ipAddress 127.0.0.1 --socket 3434 --password BOBO -nickname Cucumber`

Or any other combination of aliases. After the start, you can start sending
following commands to the server.
 - `/threads` - shows all available threads with a number of joined clients.
 - `/new [thread name]` - creates a new thread with specified name if thread
   limit is not reached.
   - if thread name is not provided `MissingCommandParameter` message is
     displayed.
   - if thread limit is reached `ThreadLimit` message is displayed.
 - `/join [thread name]` - joins you to the specified thread.
   - if thread name is not provided `MissingCommandParameter` message is
     displayed.
   - if thread name doesn't exists `UknownThread` message is displayed.
 - `/info` - shows name of the user and thread to which is client joined. Name
   of the client is provided nickname and unique id for each connected client.
 - `/del [optional thread name]` - Deletes thread you are joined or if you
   provide thread name than it deletes thread with that name.
   - if client is not joined to a thread and client didn't provide thread name
     than `MissingCommandParameter` messsage is displayed.
   - if thread name doesn't exists `UknownThread` message is displayed.
 - `/newprivate [thread name]` - creates a new private thread with specified
   name if thread limit is not reached. Private means that only the creator of
   the thread can add or kick clients to or from whitelist of the thread. If
   client is not on a whitelist client cannot see, join or delete the thread.
   - if thread name is not provided `MissingCommandParameter` message is
     displayed.
   - if thread limit is reached `ThreadLimit` message is displayed.
 - `/add [client name]` - adds client to the whitelist of the current thread if
   you are the admin of the thread.
   - if you are not the admin `NoRights` message is displayed.
   - if you are not in a thread `NoCurrentThread` message is displayed.
 - `/kick [client name]` - removes client from the whitelist of the current
   thread if you are the admin.
   - if you are not the admin `NoRights` message is displayed.
   - if you are not in a thread `NoCurrentThread` message is displayed.
 - `/leave` - leave the current thread.
 - `/exit` - disconnects from server and Terminates the client app.
Everything other is considered to be a message and if you are in a thread the
message will be sent to all clients in the thread or `NoCurrentThread` message
is displayed.
