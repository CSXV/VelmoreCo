import { AccumulativeShadows, RandomizedLight } from "@react-three/drei";

export default function ShadowsSetup() {
  return (
    <AccumulativeShadows
      temporal
      frames={100}
      color="orange"
      colorBlend={2}
      toneMapped={true}
      alphaTest={0.7}
      opacity={1}
      scale={12}
      position={[0, -0.5, 0]}
    >
      <RandomizedLight
        amount={8}
        radius={10}
        ambient={0.5}
        position={[5, 5, -10]}
        bias={0.001}
      />
    </AccumulativeShadows>
  );
}
