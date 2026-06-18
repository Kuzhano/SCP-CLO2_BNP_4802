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
        public StatusProposal GetNextState(StatusProposal currentState, AksiReview action)
        {
            var key = (currentState, action);

            // RE-CONDITION (Syarat Awal)
            if (!_transitionTable.ContainsKey(key))
            {
                throw new ArgumentException($"Transisi ditolak! Proposal dengan status '{currentState}' tidak bisa melakukan aksi '{action}'.");
            }

            StatusProposal nextState = _transitionTable[key];

            // POST-CONDITION (Syarat Akhir)
            if (!Enum.IsDefined(typeof(StatusProposal), nextState))
            {
                throw new InvalidOperationException("State tujuan tidak dikenali oleh sistem!");
            }

            return nextState;
        }

        public static StatusProposal ParseStatus(string statusString)
        {
            if (Enum.TryParse(statusString, true, out StatusProposal status))
                return status;
            return StatusProposal.PENDING; // Default
        }
    }
}