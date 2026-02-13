# winona-poker

WinonaIT Poker Assessment

## Project Structure

### `/api`

ASP.NET Core Web API (.NET 7) that handles poker game logic:

- **Controllers/** - API endpoints for dealing cards and evaluating hands
- **Models/** - Data models (Player, Deck, DealRequest, GameResult)
- **Services/** - Business logic (HandEvaluator, CardUtilities, PokerHandService)

### `/client`

React frontend built with Vite and TypeScript:

- **src/components/** - Reusable UI components (CardImages, PlayerInput)
- **src/pages/** - Page components (PokerPage)
- **src/hooks/** - Custom hooks (useDealer)
- **src/translation/** - Internationalization/translations

**Tech Stack:** React 19, TypeScript, Vite, MUI, SCSS (CSS Modules)

## Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Node.js](https://nodejs.org/) (v18+)

## Setup & Installation

### API

```bash
cd api
dotnet restore
```

### Client

```bash
cd client
npm install
```

Create a `.env` file in the `/client` folder with your configuration:

```dotenv
VITE_API_URL=http://localhost:5129
```

## Running the Project

### Start the API

```bash
cd api
dotnet run
```

The API will be available at `http://localhost:5129`.

### Start the Client

```bash
cd client
npm run dev
```

The client will be available at `http://localhost:3000`.

## Running Tests

### API Tests

```bash
cd api.tests
dotnet test
```
