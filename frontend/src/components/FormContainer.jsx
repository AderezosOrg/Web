import PropTypes from 'prop-types';

function FormContainer({ children }) {
  return (
    <div className="flex flex-col items-center justify-center bg-white w-full">
      <div className="bg-white border-2 border-rose-950 p-10 rounded-[20px] shadow-lg w-full max-w-xl">
        {children}
      </div>
    </div>
  );
}

FormContainer.propTypes = {
  children: PropTypes.node.isRequired,
};
  
export default FormContainer;
