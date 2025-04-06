/CasinoWPF
├── App.xaml                    → Einstiegspunkt der WPF-App, definiert Ressourcen und Startfenster.
|    └─ App.xaml.cs             → Code-Behind für App.xaml, Initialisierung der App. 
├── AssemblyInfo.cs             → Metadaten zur Assembly (Version, Autor etc.).
│
├── Models/                     → Datenmodelle und Spiel-Interfaces
│   ├── Player.cs               → Enthält Jetons, evtl. Name des Spielers
│   ├── GameResult.cs           → Ergebnis eines Spiels
│   ├── Game.cs                 → Interface für alle Spiele
│   └── Enums.cs                → z. B. Symboltypen, Spielergebnisse
│
├── Services/                   → Zustands- und Spiellogikverwaltung
│   ├── Session.cs              → Globale Spieler-Session (Singleton-artig)
│   ├── GameManager.cs          → Verwaltet Spielstarts, Einsätze etc.
│   ├── RelayCommand.cs         → Implementiert das Command-Pattern für WPF und ermöglicht so die Trennung von Benutzeroberfläche und Logik.
│   └── Observer.cs             → Implementierung des Observer-Patterns
│
├── Views/                      → Alle WPF-Fenster (GUI)
|   |           
│   ├── MainWindow.xaml         → Startscreen mit Jetoneingabe
|   |    └──MainWindow.xaml.cs 
|   |
│   ├── GameSelectionView.xaml  → Auswahl: Slotmachine oder BlackJack
|   |    └── GameSelectionView.xaml.cs
|   |
│   ├── SlotMachineView.xaml    → UI für SlotMachine
|   |    └──SlotMachineView.xaml.cs
|   |
│   ├── StartPage.xaml          → Startseite der App, z. B. mit "Spielen"-Button.
|   |    └── StartPage.xaml.cs
|   |
│   └── BlackJackView.xaml      → UI für BlackJack
|        └──BlackJackView.xaml.cs
│
├── ViewModels/                 → MVVM-Implementierung
│   ├── MainViewModel.cs        → Logik für MainWindow (z. B. Jetonhandling).
│   ├── StartViewModel.cs       → Verbindet StartPage mit zugehöriger Logik.
│   └── GameSelectionViewModel.cs → Logik für die Spielauswahl-View.
│  
│
├── Games/                      → Spielklassen, jeweils mit eigener Logik
│   ├── SlotMachine.cs          → Spiel-Logik für Slotmachine
│   ├── BlackJack.cs            → Spiel-Logik für BlackJack
│   └── GameFactory.cs          → Factory Pattern für die Erstellung von Spielen
│
├── Resources/ (optional)       → Icons, Bilder, Sounds, Styles etc.
│   └── casino-logo.png         → Bild für den Start der Applikation
│
└── ToDo/                       → Nächste Schritte für die Entwicklung
    └── NextSteps.md            → Beschreibung der nächsten Aufgaben

    # Nächste Schritte für die Casino-App

## 1. Navigation implementieren
- Code-Behind für Views erstellen
- Navigation zwischen den Seiten implementieren
- MainWindow als Frame-Container konfigurieren

## 2. ViewModels vervollständigen
- GameSelectionViewModel implementieren
- SlotMachineViewModel vervollständigen
- BlackJackViewModel vervollständigen
- Binding zwischen ViewModels und Views konfigurieren

## 3. UI-Design verbessern
- Hellgrauen Hintergrund für alle Seiten einstellen
- Gemeinsame Styles in ResourceDictionary auslagern
- Kartenbilder für BlackJack hinzufügen
- Symbole für SlotMachine hinzufügen (anstelle von Textdarstellung)

## 4. Datenpersistenz hinzufügen
- Spielerstatistiken implementieren (Gewinne, Verluste, etc.)
- Highscore-Liste hinzufügen
- Speicherung des Spielerstandes in XML/JSON-Datei

## 5. Spiellogik erweitern
- Zusätzliche Funktionen für BlackJack hinzufügen (Split, Insurance)
- Spezielle Gewinnlinien für SlotMachine implementieren (Diagonalen, etc.)
- Jackpot-Funktion für SlotMachine hinzufügen

## 6. Soundeffekte hinzufügen
- Hintergrundmusik
- Effekte für Gewinn/Verlust
- Kartengeräusche für BlackJack
- Walzengeräusche für SlotMachine

## 7. Animationen implementieren
- Kartenverteilungsanimation für BlackJack
- Walzendrehanimation für SlotMachine
- Übergangsanimationen zwischen Bildschirmen

## 8. Testing
- Unit-Tests für die Spiellogik erstellen
- UI-Tests für die Benutzeroberfläche erstellen
- Spielbalance testen und anpassen

## 9. Dokumentation
- Code-Kommentare vervollständigen
- Benutzeranleitung erstellen
- Klassendiagramm aktualisieren

## 10. Erweiterungen für die Zukunft
- Weitere Spiele hinzufügen (Poker, Roulette)
- Mehrspieler-Modus implementieren
- Optionale Online-Funktionen (z.B. globale Highscores)