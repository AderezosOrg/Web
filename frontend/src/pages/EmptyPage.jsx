import { useState } from 'react'
import { Formik, Form } from 'formik';
import Button from '../components/Button';
import InputField from '../components/Input';

export default function EmptyPage() {  
  const [count, setCount] = useState(0);
  const [isNameCorrect, setIsNameCorrect] = useState(true);

  return (
    <div className="flex flex-col h-screen w-screen items-center justify-center">
      <p className="font-roboto text-black text-lg">
        Empty page
      </p>
      <div className="flex flex-col items-center">
        <Formik
          initialValues={{ name: ''}}
          onSubmit={(values) => {
            if (!values.name.trim())
              {
                setIsNameCorrect(false);
                return;
              } else {
                setIsNameCorrect(true);
              }
            console.log('Submitted values:', values);
          }}
        >
          {() => (
            <Form className="flex flex-col gap-4">
              <InputField
              id="name"
              name="name"
              placeholder="Nombre..."
              label="Nombre"
              isCorrect={isNameCorrect}
            />
            </Form>
          )}
        </Formik>
        <div className="card p-6">
          <Button
            type={"common"}
            onClick={() => setCount((count) => count + 1)}
          >
            Count is {count}
          </Button>
        </div>
      </div>
    </div>
  );
}
