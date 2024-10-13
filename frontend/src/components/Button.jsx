import PropTypes from 'prop-types';

function Button ({ children, className, onClick, isSubmit, type, text }) {
  const baseStyles = {
    common:
      'bg-rose-900 text-white font-regular py-2 px-4 rounded-[8px] hover:bg-rose-700 transition duration-300'
  };

  const buttonClass = `${baseStyles[type]} ${className}`;

  return (
    <button
      className={`${buttonClass} transition-transform duration-150 active:scale-95`}
      type={isSubmit ? 'submit' : 'button'}
      onClick={onClick}
    >
      {text ? text : children}
    </button>
  );
};

Button.propTypes = {
  children: PropTypes.node,
  className: PropTypes.string,
  onClick: PropTypes.func,
  isSubmit: PropTypes.bool,
  type: PropTypes.oneOf(['common']),
  text: PropTypes.string,
};

export default Button;
