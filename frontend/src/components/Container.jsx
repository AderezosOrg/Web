import PropTypes from 'prop-types';

function Container({ children }) {
  return (
    <div className="flex flex-col items-center justify-center bg-white w-full">
      <div className="bg-white border-2 border-rose-950 p-8 rounded-[20px] shadow-lg shadow-black/20 w-full max-w-2xl
      ">
        {children}
      </div>
    </div>
  );
}

Container.propTypes = {
  children: PropTypes.node.isRequired,
};
  
export default Container;
