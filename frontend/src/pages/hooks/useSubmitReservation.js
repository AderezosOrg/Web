import { useCallback } from 'react';
import { createReservation } from '../../services/reservationService';

const useSubmitReservation = (setFormStatus) => {
  const submitCompleteReservation = useCallback(async (reservationDetails) => {
    try {
      const response = await createReservation(reservationDetails);
      setFormStatus({ success: true, message: response.message || 'Reserva creada con éxito' });
    } catch (error) {
      setFormStatus({ success: false, message: error.message });
    }
  }, [setFormStatus]);

  return { submitCompleteReservation };
};

export default useSubmitReservation;
