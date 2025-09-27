export default function Jbody({ geometry, material }: any) {
  return (
    <mesh
      geometry={geometry}
      receiveShadow
      castShadow
      material={material}
      // material-color={"white"}
    >
      {
        <meshPhysicalMaterial
          color="#af823b"
          metalness={1}
          roughness={0.2}
          emissive="#af823b"
        />
      }
    </mesh>
  );
}
