
export default function NotFound() {  

  return (
    <div className='mx-auto h-full w-full p-4 lg:max-w-7xl lg:px-6 lg:py-8'>
      <article className='grid h-full place-content-center text-center text-5xl font-semibold'>
        <div className='flex flex-col gap-5 rounded-3xl bg-rose-900 p-20 text-white shadow-xl'>
          <h1>404</h1>
          <h2>Page not found</h2>
        </div>
      </article>
    </div>
  );
}
