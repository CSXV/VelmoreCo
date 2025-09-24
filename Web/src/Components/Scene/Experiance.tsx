import {
  useGLTF,
  Caustics,
  CubeCamera,
  Environment,
  OrbitControls,
  MeshTransmissionMaterial,
} from "@react-three/drei";
import { RGBELoader } from "three-stdlib";
import { Canvas, useLoader } from "@react-three/fiber";
import { EffectComposer, Bloom } from "@react-three/postprocessing";

import Gem from "./Gem";
import LightSetup from "./LightSetup";
import ShadowsSetup from "./ShadowsSetup";
import Jbody from "./Body";

// -----------------------------------------------------------------------
export default function Experiance({ params }: any) {
  const texture = useLoader(
    RGBELoader,
    "https://dl.polyhaven.org/file/ph-assets/HDRIs/hdr/1k/aerodynamics_workshop_1k.hdr",
  );

  const { scene, nodes, materials } = useGLTF(
    `/public/Models/rings/00${params}.glb`,
  );
  // const diamond = useGLTF(`/public/Models/dflat.glb`);
  // const { scene, nodes, materials } = useGLTF(`/public/Models/rings/001.glb`);

  // nodes.gem.visible = false;
  // nodes.body.visible = false;

  // -------------------------------------
  if (!texture && !scene && !nodes) {
    return (
      <div>
        {" "}
        <h2>Loading...</h2>{" "}
      </div>
    );
  }

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
        camera={{ position: [-5, 1, 5], fov: 40 }}
      >
        <color attach="background" args={["#f0f0f0"]} />
        <LightSetup />

        <CubeCamera resolution={256} frames={1} envMap={texture}>
          {(texture) => (
            <Caustics
              backfaces
              color={"white"}
              position={[0, -0.5, 0]}
              lightSource={[5, 5, -10]}
              worldRadius={0.1}
              ior={1.8}
              backfaceIor={1.1}
              intensity={0.1}
            >
              <group scale={20} position={[0, 1, 0]} rotation={[0, 5.5, 0]}>
                {
                  // <primitive object={scene} />
                }
                <Gem texture={texture} geometry={nodes.diamonds.geometry} />
                <Jbody
                  geometry={nodes.ring.geometry}
                  material={materials.ring}
                />
              </group>

              {
                // <Gem
                //   texture={texture}
                //   geometry={diamond.nodes.Diamond_1_0.geometry}
                // />
                // TEST GEM
                // <mesh
                //   castShadow
                //   ref={ref}
                //   geometry={diamond.nodes.Diamond_1_0.geometry}
                //   rotation={[0, 0, 0.715]}
                //   position={[0, -0.175 + 0.5, 0]}
                // >
                //   <MeshRefractionMaterial
                //     envMap={texture}
                //     bounces={3}
                //     ior={2.75}
                //     fresnel={1}
                //     color={"white"}
                //     aberrationStrength={0.01}
                //     toneMapped={false}
                //   />
                // </mesh>
              }
            </Caustics>
          )}
        </CubeCamera>

        {
          // TEST BALL
          // <Caustics
          //   color="#FF8F20"
          //   position={[0, -0.5, 0]}
          //   lightSource={[5, 5, -10]}
          //   worldRadius={0.01}
          //   ior={1.2}
          //   intensity={0.005}
          // >
          //   <mesh castShadow receiveShadow position={[-2, 0.5, -1]} scale={0.5}>
          //     <sphereGeometry args={[1, 64, 64]} />
          //     <MeshTransmissionMaterial
          //       resolution={1024}
          //       distortion={0.25}
          //       color="#FF8F20"
          //       thickness={1}
          //       anisotropy={1}
          //     />
          //   </mesh>
          // </Caustics>
        }

        <ShadowsSetup />

        <OrbitControls
          makeDefault
          autoRotate
          autoRotateSpeed={0.2}
          minPolarAngle={0}
          maxPolarAngle={Math.PI / 2}
        />

        <Environment files="https://dl.polyhaven.org/file/ph-assets/HDRIs/hdr/1k/aerodynamics_workshop_1k.hdr" />

        {
          <EffectComposer>
            <Bloom luminanceThreshold={1} intensity={2} levels={9} mipmapBlur />
          </EffectComposer>
        }
      </Canvas>
    </div>
  );
}
