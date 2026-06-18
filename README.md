# Sistem Manajemen Proposal (CLI Version) - Tugas Besar Konstruksi PL (CLO2)

## Overview
Repository ini berisi implementasi **Fase 1 (CLO2)** untuk Tugas Besar mata kuliah Konstruksi Perangkat Lunak. 

Pada iterasi ini, aplikasi dibangun murni berbasis **Command Line Interface (CLI)**. Sesuai batasan sistem CLO2, setiap fitur utama dikembangkan sebagai modul yang terisolasi (*decoupled*) dan mengeksploitasi mekanisme *local storage* berbasis `.json`. 

Fase ini berfokus pada keandalan logika *backend*, *Design by Contract*, dan *testing*, sebelum nantinya diintegrasikan secara penuh menggunakan antarmuka grafis (GUI) Windows Form pada fase CLO4.

## Tim Pengembang & Distribusi Modul
Sistem ini memecah monolitik menjadi 5 modul terpisah. Setiap *engineer* (mahasiswa) bertanggung jawab atas *branch* Git, pengembangan logika, *testing*, serta injeksi *Defensive Programming* pada modul masing-masing.

| Nama | NIM | Tanggung Jawab | Teknik Konstruksi (Max 2) |
| :--- | :--- | :--- | :--- |
| [Nama Anda] | [NIM Anda] | Modul 1: IAM & Main Menu CLI | API & Automata |
| [Nama Anggota 2] | [NIM Anggota 2] | Modul 2: Pendaftaran Proposal | Parameterization & Runtime Config |
| [Nama Anggota 3] | [NIM Anggota 3] | Modul 3: Review & Penilaian | Automata & Table-Driven |
| [Nama Anggota 4] | [NIM Anggota 4] | Modul 4: Dashboard & Tracking | Code Reuse & Runtime Config |
| [Nama Anggota 5] | [NIM Anggota 5] | Modul 5: Pengarsipan & Ekspor | Parameterization & API |

## Development Stack & Arsitektur
- **Language** : `C#`
- **Framework**: `.NET 6.0` / `.NET 8.0`
- **Database** : `Local JSON (.json)` - *File-based storage bypass*
- **Testing** : `xUnit` / `NUnit` (Digunakan untuk Unit & Performance Testing)
- **Tools** : `Visual Studio`, `Git`

## Metrik Kepatuhan CLO2 (*Compliance Checklist*)
Untuk memenuhi standar *Software Quality Assurance* pada fase ini, sistem telah mengimplementasikan:
- [x] **Decoupled Architecture**: Modul dapat berdiri sendiri dan berinteraksi via *Dependency Injection* atau integrasi file statis.
- [x] **Defensive Programming (DbC)**: Penanganan *Pre-condition*, *Post-condition*, dan validasi *null/invariant* di setiap entri modul untuk mencegah *system crash*.
- [x] **Unit Testing**: Pengujian skenario fungsionalitas logika di masing-masing sub-kelas.
- [x] **Performance Testing**: Pengukuran *runtime* pada proses I/O JSON dan logika *looping/table-driven*.
- [x] **Git Flow Standard**: Bukti *commit* individu pada *branch* masing-masing sebelum proses *Merge* ke `main/master`.

## Cara Menjalankan Program
1. Lakukan *clone repository* ini ke *environment* lokal Anda.
2. Buka *solution* `.sln` menggunakan Visual Studio.
3. Lakukan *Build Solution* (Ctrl+Shift+B) untuk me- *restore* *dependencies* (misal: `System.Text.Json`).
4. Jadikan `Program.cs` / Modul 1 sebagai *Startup Object*.
5. Eksekusi program (F5). Aplikasi akan berjalan di terminal/konsol, dan *file database* seperti `proposal_db.json` akan otomatis di- *generate* pada direktori eksekusi jika belum ada.
