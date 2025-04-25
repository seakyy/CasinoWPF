# 📁 Project Structure: CasinoWPF

Visual Studio project structure for the implementation of the Casino application featuring **BlackJack** and **SlotMachine**. The project uses **WPF** and the **MVVM pattern**.

```plaintext
/CasinoWPF
├── App.xaml                  → Entry point of the WPF app, defines resources and startup window
├── App.xaml.cs              → Code-behind for App.xaml, initializes the application
├── AssemblyInfo.cs          → Assembly metadata (version, author, etc.)
│
├── Models/                  → Data models for game logic
│   ├── Player.cs            → Holds balance (chips) and player logic
│   ├── GameResult.cs        → Stores game outcome, winnings, bet info
│   ├── Game.cs              → Interface for all games
│   ├── Card.cs              → Data structure for playing cards (BlackJack)
│   └── Enums.cs             → Enum types for card symbols, outcomes, game modes
│
├── Services/                → Manages global game state
│   ├── Session.cs           → Global player session (singleton-like)
│   ├── GameManager.cs       → Core control for game launch & bets
│   ├── RelayCommand.cs      → ICommand implementation for MVVM (Command Pattern)
│   ├── Observer.cs          → Observer pattern implementation
│   └── GameLogService.cs    → Singleton for logging game activity
│
├── Views/                   → WPF Views (XAML) for the graphical UI
│   ├── MainWindow.xaml      → Frame container and navigation
│   ├── StartPage.xaml       → Landing screen with chip input and start
│   ├── GameSelectionView.xaml → Game selection menu
│   ├── SlotMachineView.xaml → SlotMachine user interface
│   ├── BlackJackView.xaml   → BlackJack user interface
│   └── GameLogWindow.xaml   → Secondary window for viewing game log
│
├── ViewModels/              → MVVM ViewModels
│   ├── MainViewModel.cs     → Manages initial balance and startup state
│   ├── StartViewModel.cs    → Logic for StartPage
│   ├── GameSelectionViewModel.cs → Logic for selecting a game
│   ├── SlotMachineViewModel.cs   → SlotMachine logic and spin handling
│   └── BlackJackViewModel.cs     → Game logic, AI opponents, scoring
│
├── Games/                   → Game implementations
│   ├── SlotMachine.cs       → Game rules, random logic & payouts
│   ├── BlackJack.cs         → Card logic, player actions (hit, stand, double down)
│   └── GameFactory.cs       → Factory for creating game instances
│
├── Resources/               → Icons, images, and static content
│   ├── Cherry.jpg, Lemon.jpg, ... → SlotMachine symbols
│   └── casino-logo.png      → Logo for start screen
│
├── Tests/                   → Unit tests (TDD)
│   ├── SlotMachineTest.cs   → Tests for SlotMachine logic (start, win, spin)
│   └── BlackJackTest.cs     → Tests for BlackJack (hit, double down)
│
└── .github/workflows/
    └── dotnet.yml           → GitHub Actions CI workflow (build + test)
```

---

📦 **Technologies:** WPF, .NET 8.0, MVVM, MSTest, GitHub Actions

🧪 **TDD Applied:** Yes → SlotMachine & BlackJack logic covered

🔄 **CI/CD:** Automated via GitHub Actions on every `main` push

📝 **Future Extensions:** Additional games, admin panel, highscore tracking, player profiles, persistence (DB)

---
