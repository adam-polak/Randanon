export default function Home() {
  return (
    <main>
      <h1>Hello world!</h1>
    </main>
  );
}

{/*
  Flow
  ----
  1. a. Check user for cookies
          if none then step 2
      b. validate user found in cookies
        if incorrect info delete from cookies and step 2
  2. Create user and store in cookies if one does not exist
  */}