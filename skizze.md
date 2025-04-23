# 📁 Projektstruktur: CasinoWPF

Visual Studio Projektstruktur zur Umsetzung der Casino-Applikation mit den Spielen **BlackJack** und **SlotMachine**. Das Projekt verwendet **WPF** und das **MVVM-Muster**.

```plaintext
/CasinoWPF
├── App.xaml                  → Einstiegspunkt der WPF-App, definiert Ressourcen und Startfenster
├── App.xaml.cs              → Code-Behind für App.xaml, Initialisierung der App
├── AssemblyInfo.cs          → Metadaten zur Assembly (Version, Autor etc.)
│
├── Models/                  → Datenmodelle für Spielmechaniken
│   ├── Player.cs            → Enthält Guthaben (Jetons) und Spielerlogik
│   ├── GameResult.cs        → Speichert Spielausgang, Gewinn, Einsatz usw.
│   ├── Game.cs              → Interface für alle Spiele
│   ├── Card.cs              → Datenstruktur für Spielkarten (BlackJack)
│   └── Enums.cs             → Enum-Typen für Kartensymbole, Spielergebnisse, Spielarten
│
├── Services/                → Verwaltung globaler Spielzustände
│   ├── Session.cs           → Globale Spieler-Session (Singleton-artig)
│   ├── GameManager.cs       → Zentrale Steuerung von Spielstarts & Einsätzen
│   ├── RelayCommand.cs      → Umsetzung von ICommand für MVVM (Command Pattern)
│   ├── Observer.cs          → Implementierung des Observer-Patterns
│   └── GameLogService.cs    → Singleton für Protokollierung aller Spielverläufe
│
├── Views/                   → WPF Views (XAML) für die grafische Oberfläche
│   ├── MainWindow.xaml      → Fenster mit Frame und Navigation
│   ├── StartPage.xaml       → Startseite mit Jetoneingabe und Spielstart
│   ├── GameSelectionView.xaml → Spielauswahlmenü
│   ├── SlotMachineView.xaml → Benutzeroberfläche für SlotMachine
│   ├── BlackJackView.xaml   → Benutzeroberfläche für BlackJack
│   └── GameLogWindow.xaml   → Zweites Fenster zur Anzeige des Spielverlaufs
│
├── ViewModels/              → ViewModels nach MVVM-Prinzip
│   ├── MainViewModel.cs     → Jetonverwaltung, Initialzustand
│   ├── StartViewModel.cs    → Logik für StartPage
│   ├── GameSelectionViewModel.cs → Spielauswahl-Logik
│   ├── SlotMachineViewModel.cs   → Spiel- und Spinlogik für SlotMachine
│   └── BlackJackViewModel.cs     → Spiellogik, KI-Gegner, Punkteberechnung
│
├── Games/                   → Eigentliche Spiel-Implementierungen
│   ├── SlotMachine.cs       → Spielregeln, Zufallslogik & Auszahlungen
│   ├── BlackJack.cs         → Kartenlogik, Spieleraktionen (hit, stand, doubledown)
│   └── GameFactory.cs       → Factory zur Erstellung von Spielinstanzen
│
├── Resources/               → Icons, Bilder und statische Inhalte
│   ├── Cherry.jpg, Lemon.jpg, ... → Symbole für SlotMachine
│   └── casino-logo.png      → Logo für Startbildschirm
│
├── Tests/                   → Unit Tests (TDD)
│   ├── SlotMachineTest.cs   → Tests für SlotMachine-Logik (Start, Gewinn, Spin)
│   └── BlackJackTest.cs     → Tests für BlackJack-Abläufe (Hit, DoubleDown)
│
└── .github/workflows/
    └── dotnet.yml           → GitHub Actions Workflow für CI (Build + Test)
```

---

📦 **Technologien:** WPF, .NET 8.0, MVVM, MSTest, GitHub Actions

🧪 **TDD umgesetzt:** Ja → SlotMachine & BlackJack Tests

🔄 **CI/CD:** Automatisch über GitHub bei jedem Push auf `main`

📝 **Erweiterbar um:** weitere Spiele, Adminpanel, Highscore, Spielerprofil, Persistenz (Datenbank)

---
