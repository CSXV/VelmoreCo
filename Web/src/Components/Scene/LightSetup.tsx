export default function LightSetup() {
  return (
    <>
      <ambientLight intensity={0.5 * Math.PI} />
      <spotLight decay={0} position={[5, 5, -10]} angle={0.15} penumbra={1} />
      <pointLight decay={0} position={[-10, -10, -10]} />
    </>
  );
}
