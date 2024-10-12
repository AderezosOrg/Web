import { Field } from 'formik';
import PropTypes from 'prop-types';

function InputField ({ id, label, name, placeholder, type, isCorrect, isDisabled }) {
    return (
    <label htmlFor={id} className="block mb-2">
      <span className='px-4 text-[20px] text-neutral-950 font-bold'>
        {label}
        <span className='text-red-700'> *</span>
      </span>
      <Field
        id={id}
        name={name}
        type={type}
        className={`bg-[#EEEEEE] w-full rounded-[20px] focus:ring-rose-700 focus:border-rose-700 block p-2.5 px-5 py-3 outline-none transition duration-150 ${isCorrect ? 'border-2 border-rose-950' : 'border-2 border-red-500'}`}
        placeholder={placeholder}
        disabled={isDisabled}
        required
      />
    </label>
  );
}

InputField.propTypes = {
    id: PropTypes.string.isRequired,
    label: PropTypes.string.isRequired,
    name: PropTypes.string.isRequired,
    placeholder: PropTypes.string.isRequired,
    type: PropTypes.string,
    isCorrect: PropTypes.bool,
    isDisabled: PropTypes.bool,
};

export default InputField;
