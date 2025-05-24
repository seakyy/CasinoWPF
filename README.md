# CasinoWPF – Digital Casino Simulation with WPF

# Showcase
![grafik](https://github.com/user-attachments/assets/edcaf44c-be89-465c-bf82-ea97843a95bc)
![grafik](https://github.com/user-attachments/assets/dc723a32-81ac-44c6-9ccd-d75f6d6823d1)




# Overview Diagram
![diagram](https://github.com/user-attachments/assets/c92599cf-65cf-4356-b244-ae4034fc3e45)



**Submission Deadline:** April 24, 2025  
**Project Start:** April 3, 2025 **/ Release:** April 25, 2025

A C# project based on **.NET 8.0** and **WPF**, simulating two classic casino games: **Slot Machine** and **BlackJack**. The project follows the **MVVM architectural pattern** and was developed using **agile methods**. All game logic, UI, and user interactions are modular and extensible for future updates.

---

## Project Overview

- Choice between two casino games: **Slot Machine** and **BlackJack**
- User-defined starting amount, converted to virtual chips
- Game history is tracked in a separate log window (GameLog)
- Bets & winnings follow defined rulesets
- Return to game selection after each round
- Project goal: Modular, testable, and CI-supported application

---

## Technical Features

- **Technology:** .NET 8.0 (WPF), C#
- **Architecture:** MVVM with Commands, Services & Observer Pattern
- **Test Coverage:** MSTest UnitTests for SlotMachine & BlackJack
- **CI/CD:** GitHub Actions / GitLab CI/CD with Build & Test Workflow

---

## Project Structure (Excerpt)

```plaintext
/src
├── CasinoWPF
│   ├── Models/           → Players, Cards, Game Results
│   ├── Games/            → SlotMachine.cs, BlackJack.cs
│   ├── Services/         → Session, GameManager, GameLogService
│   ├── ViewModels/       → View controllers (e.g. SlotMachineViewModel)
│   └── Views/            → WPF XAML Views (e.g. BlackJackView.xaml)
│
├── CasinoWPF-Tests
│   ├── SlotMachineTest.cs
│   └── BlackJackTest.cs
│
└── .github/workflows/dotnet.yml → CI/CD workflow for build & test
```

---

## Getting Started

1. Clone the repository
2. Open the solution in Visual Studio:  
   `src/CasinoWPF/CasinoWPF.sln`
3. Run the project with `F5`
4. Enter your starting balance and choose a game

---

## Future Enhancements

- Add more games (e.g. Roulette, Poker)
- Implement user profiles and highscore tracking
- Add persistence through database support
- Include visual animations and sound effects

---

This project was developed as a school assignment using agile principles and modern .NET technologies.
