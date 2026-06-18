using System;
using System.Collections.Generic;

namespace ModulReviewPenilaian
{
    public enum StatusProposal { PENDING, DITERIMA, DITOLAK, REVISI }
    public enum AksiReview { TERIMA, TOLAK, MINTA_REVISI, KEMBALIKAN_PENDING }

    public class ReviewStateMachine
    {
        // TK: Table-driven Construction
        // Tabel transisi Automata yang memetakan (CurrentState, Action) -> NextState
        private readonly Dictionary<(StatusProposal, AksiReview), StatusProposal> _transitionTable;

        public ReviewStateMachine()
        {
            _transitionTable = new Dictionary<(StatusProposal, AksiReview), StatusProposal>
            {
                { (StatusProposal.PENDING, AksiReview.TERIMA), StatusProposal.DITERIMA },
                { (StatusProposal.PENDING, AksiReview.TOLAK), StatusProposal.DITOLAK },
                { (StatusProposal.PENDING, AksiReview.MINTA_REVISI), StatusProposal.REVISI },

                { (StatusProposal.REVISI, AksiReview.TERIMA), StatusProposal.DITERIMA },
                { (StatusProposal.REVISI, AksiReview.TOLAK), StatusProposal.DITOLAK },

                { (StatusProposal.DITOLAK, AksiReview.KEMBALIKAN_PENDING), StatusProposal.PENDING }
            };
        }

        // TK: Automata
        // Method untuk mengeksekusi transisi state
        public StatusProposal GetNextState(StatusProposal currentState, AksiReview action)
        {
            var key = (currentState, action);
            if (_transitionTable.TryGetValue(key, out StatusProposal nextState))
            {
                return nextState;
            }
            throw new InvalidOperationException($"Transisi tidak valid: {currentState} tidak bisa melakukan aksi {action}.");
        }

        public static StatusProposal ParseStatus(string statusString)
        {
            if (Enum.TryParse(statusString, true, out StatusProposal status))
                return status;
            return StatusProposal.PENDING; // Default
        }
    }
}