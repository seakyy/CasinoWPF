# CasinoWPF – Digitale Spielsimulation in WPF

**Abgabetermin:** 24.04.2025  
**Projektstart / Release:** 20.04.2025

Ein C#-Projekt auf Basis von **.NET 8.0** und **WPF**, welches zwei klassische Casino-Spiele simuliert: **SlotMachine** und **BlackJack**. Das Projekt folgt dem **MVVM-Architekturprinzip** und wurde mit **agilen Methoden** umgesetzt. Sämtliche Spiellogik, Oberfläche und Benutzerinteraktionen sind modular entworfen, sodass zukünftige Erweiterungen leicht möglich sind.

---

## Projektübersicht

- Auswahl zwischen zwei Casino-Spielen: **SlotMachine** und **BlackJack**
- Startbetrag frei wählbar → wird in Jetons umgewandelt
- Spielverlauf wird im separaten Fenster protokolliert (GameLog)
- Einsätze & Gewinne gemäß Regelwerk
- Rückkehr zur Spielauswahl nach jeder Runde
- Projektziel: Modulare, testbare & CI-gestützte Anwendung

---

## Technische Merkmale

- **Technologie:** .NET 8.0 (WPF), C#
- **Architektur:** MVVM mit Commands, Services & Observer Pattern
- **Testabdeckung:** MSTest UnitTests für SlotMachine & BlackJack
- **CI/CD:** GitHub Actions mit Build & Test Workflow

---

## Projektstruktur (Ausschnitt)

```plaintext
/src
├── KoteskiOlmesLB-426
│   ├── Models/           → Spieler, Karten, Spielresultate
│   ├── Games/            → SlotMachine.cs, BlackJack.cs
│   ├── Services/         → Session, GameManager, GameLogService
│   ├── ViewModels/       → Steuerung der Views (z. B. SlotMachineViewModel)
│   └── Views/            → WPF-XAML-Views (z. B. BlackJackView.xaml)
│
├── KoteskiOlmesLB-426-Tests
│   ├── SlotMachineTest.cs
│   └── BlackJackTest.cs
│
└── .github/workflows/dotnet.yml → CI/CD-Workflow für Build & Test
```

---

## Projektstart

1. Repository klonen
2. Visual Studio öffnen und die Lösung laden:  
   `src/KoteskiOlmesLB-426/KoteskiOlmesLB-426.sln`
3. Mit `F5` ausführen
4. Startbetrag eingeben und Spiel wählen

---

## Weiterentwicklungsmöglichkeiten

- Erweiterung um zusätzliche Spiele (z. B. Roulette, Poker)
- Einführung von Benutzerprofilen und Highscore
- Persistenz durch Datenbankanbindung
- Visuelle Animationen und akustisches Feedback

---

Dieses Projekt wurde im Rahmen einer schulischen Arbeit nach agilen Prinzipien konzipiert und umgesetzt.
