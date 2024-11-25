# Blackjack Game

This is a console-based Blackjack game implemented in C#. The game allows a player to play Blackjack against a dealer with various functionalities such as placing bets, splitting hands, and doubling down.

## Features

- **Start Game**: Begin a new game of Blackjack.
- **Place Bet**: The player can place a bet to start the game. Negative bets are not allowed.
- **Hit**: The player can draw additional cards to try to get closer to 21 without going over.
- **Stay**: The player can choose to keep their current hand and end their turn.
- **Split**: If the player has two cards of the same rank, they can split them into two separate hands.
- **Double Down**: The player can double their bet and receive exactly one more card.
- **Insurance Bet**: If the dealer's face-up card is an Ace, the player can place an insurance bet.
- **Dealer Turn**: The dealer will draw cards according to the rules of Blackjack.
- **Determine Winner**: The game will determine the winner based on the hands of the player and the dealer.
- **Exit**: The player can exit the game at any time.

## How to Play

1. **Start the Game**: Run the application and choose "Start Game" from the main menu.
2. **Place Your Bet**: Enter a valid bet amount. Negative bets are not allowed.
3. **Gameplay**: Choose from the available options (Hit, Stay, Split, Double Down) to play your hand.
4. **Dealer's Turn**: The dealer will play their hand according to the rules of Blackjack.
5. **Determine Winner**: The game will determine the winner and update the player's balance.
6. **Exit**: Choose "Exit" from the main menu to end the game.

## Models

### Card

The `Card` class represents a single playing card with a rank and suit.


### Deck

The `Deck` class represents a deck of playing cards. It provides methods to shuffle the deck and draw cards.


### Hand

The `Hand` class represents a hand of cards held by a player or dealer. It provides methods to add cards to the hand and calculate the hand's value.


### Player

The `Player` class represents a player in the game. It includes properties for the player's balance, current hand, and methods for placing bets and performing actions.


### Dealer

The `Dealer` class represents the dealer in the game. It includes properties for the dealer's hand and methods for performing dealer actions.

## Controllers

### GameController

The `GameController` class manages the game logic, including dealing cards, handling player actions, and determining the winner.


## Views

### GameView

The `GameView` class handles the user interface and interactions with the player. It displays the game state and prompts the player for actions.

Spectre console was  used for text-based UI.

