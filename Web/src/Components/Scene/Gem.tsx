import { MeshRefractionMaterial } from "@react-three/drei";

export default function Gem({ texture, geometry }: any) {
  return (
    <mesh geometry={geometry} position={[0, 0, 0.12]}>
      {
        <MeshRefractionMaterial
          envMap={texture}
          bounces={4}
          ior={2.75}
          fresnel={1}
          color={"white"}
          aberrationStrength={0.01}
          toneMapped={false}
        />
      }
    </mesh>
  );
}
