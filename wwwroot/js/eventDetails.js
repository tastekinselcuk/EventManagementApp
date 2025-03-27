document.addEventListener('DOMContentLoaded', function () {
    const eventId = document.querySelector('[data-event-id]').dataset.eventId;
    loadParticipants();
    loadAvailableParticipants();

    document.getElementById('addParticipantBtn').addEventListener('click', addParticipant);

    function loadParticipants() {
        fetch(`/Event/GetParticipants/${eventId}`)
            .then(response => response.json())
            .then(data => {
                const list = document.getElementById('participantsList');
                list.innerHTML = data.map(p => `
                    <div class="card mb-2">
                        <div class="card-body d-flex justify-content-between align-items-center">
                            <span>${p.firstName} ${p.lastName}</span>
                            <button class="btn btn-danger btn-sm" onclick="removeParticipant(${p.id})">
                                Remove
                            </button>
                        </div>
                    </div>
                `).join('');
            });
    }

    function loadAvailableParticipants() {
        fetch(`/Participant/GetAvailableParticipants?eventId=${eventId}`)
            .then(response => response.json())
            .then(data => {
                const select = document.getElementById('participantSelect');
                select.innerHTML = data.map(p => 
                    `<option value="${p.id}">${p.firstName} ${p.lastName}</option>`
                ).join('');
            });
    }

    function addParticipant() {
        const participantId = document.getElementById('participantSelect').value;
        fetch('/Event/AddParticipant', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
            },
            body: JSON.stringify({ eventId, participantId })
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                loadParticipants();
                loadAvailableParticipants();
                bootstrap.Modal.getInstance(document.getElementById('addParticipantModal')).hide();
            }
        });
    }

    window.removeParticipant = function(participantId) {
        if (confirm('Are you sure you want to remove this participant?')) {
            fetch('/Event/RemoveParticipant', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
                },
                body: JSON.stringify({ eventId, participantId })
            })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    loadParticipants();
                    loadAvailableParticipants();
                }
            });
        }
    }
});
