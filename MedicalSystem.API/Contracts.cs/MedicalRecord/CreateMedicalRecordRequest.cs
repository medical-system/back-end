using MedicalSystem.API.Contracts.cs.Prescription;
using MedicalSystem.API.Entities;

namespace MedicalSystem.API.Contracts.cs.MedicalRecord
{
    public record CreateMedicalRecordRequest
    (
        string Complaint,
        string Diagnosis,
        string Treatment,
        string VitalSigns,
        List<CreatePrescriptionRequest> Prescriptions
    );
}
