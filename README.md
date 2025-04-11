# 🎰 CasinoWPF
Notiz: Skizze ist nicht Up-to-Date

Ein WPF-Projekt in C# zur Simulation eines kleinen digitalen Casinos mit zwei Spielen:
- **SlotMachine** 🎰
- **BlackJack** 🃏

## 🛠 Features
- Start mit Eingabe eines frei wählbaren Geldbetrags (in Jetons umgewandelt)
- Auswahl zwischen zwei Spielen: SlotMachine & BlackJack
- Einsätze und Gewinne je nach Spielmechanik
- Zurück zur Spielauswahl nach jeder Runde
- Modular aufgebaut: neue Spiele können leicht ergänzt werden

## 📁 Projektstruktur
```
/Models       → Datenmodelle & Interfaces
/Views        → GUI-Fenster (WPF-XAML)
/Games        → Spielimplementierungen (z. B. SlotMachine.cs)
/Services     → Zustandsverwaltung & Helfer
/ViewModels   → (Optional, für MVVM)
```

## ▶️ Starten
1. Öffne die `.sln`-Datei in Visual Studio
2. Starte die Anwendung mit `F5`
3. Gib deinen Startbetrag ein
4. Wähle ein Spiel und hab Spaß!

## ➕ Erweiterungsideen
- Highscore-System
- Weitere Spiele (z. B. Poker, Roulette)
- Statistikseite mit Spielhistorie
- Soundeffekte & Animationen

---
Entwickelt mit ❤️ für eine Informatikprojektarbeit mit agilen Methoden.
