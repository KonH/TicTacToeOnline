# Tic Tac Toe Online

Client-server Tic Tac Toe example game.

## Requirements

- .NET Core runtime 2.1.4+
- Unity 2018.1.0f2

## Summary

Contains server, console and Unity client.

## Run

1. Start server - ```TicTacToe/_startServer.sh```
2. Start console client - ```TicTacToe/_startClient.sh```
3. Start/build Unity client from ```UnityClient```

## Limitations/TODOs

- One session per time
- No way to properly close session on client side (look at 'reset' server endpoint)
- Needs refactoring
- May be docker support
