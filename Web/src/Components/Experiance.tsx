import { Canvas } from "@react-three/fiber";
import { OrbitControls } from "@react-three/drei";

export default function Experiance() {
  return (
    <div style={{ height: "100%" }}>
      <Canvas
        style={{
          // maxHeight: "442px",
          // maxHeight: "70vh",
          // height: "100%",
          height: "70vh",
          width: "100%",
          borderRadius: "10px",
        }}
        shadows
        camera={{ position: [0, 0, 5], fov: 20 }}
      >
        <OrbitControls />
        <ambientLight intensity={0.5} />
        <directionalLight castShadow position={[0, 0, 10]} intensity={1} />

        <mesh rotation={[2, 2, 0]}>
          <boxGeometry args={[1, 1, 1]} />
          <meshPhysicalMaterial color={"blue"} />
        </mesh>
      </Canvas>
    </div>
  );
}
