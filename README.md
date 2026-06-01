# Proyecto 3_EquipoX — Build & Package + Seguridad en GitHub

## Descripción
Genera un artefacto reproducible, produce SBOM, firma con cosign y publica en Harbor local. Pipeline automatizado con GitHub Actions valida firma y SBOM.

## Requisitos
- Docker >= 24
- Docker Compose
- cosign (https://github.com/sigstore/cosign)
- syft (para SBOM) o cyclonedx
- GitHub Actions (repo con secrets)
- Harbor (local via Docker Compose) o registro Docker accesible

## Estructura del repo
- /app         -> código fuente
- /docker      -> Dockerfile
- /ci          -> workflows (.github/workflows)
- /sbom        -> SBOMs generados
- /sign        -> claves cosign (ejemplo, NO subir claves reales)
- /docs        -> README, evidence.md

## Variables / Secrets (GitHub)
- REGISTRY: `harbor.local` (host)
- REPO: `project3/equipoX`
- HARBOR_USER
- HARBOR_PASS
- COSIGN_KEY (contenido de la clave privada)
- COSIGN_PASSWORD

## Comandos locales (rápido)
1. Build local:
   docker build -t local-image:latest -f docker/Dockerfile .
2. Tag para Harbor:
   docker tag local-image:latest harbor.local/project3/equipoX:latest
3. Push:
   docker login harbor.local -u $HARBOR_USER -p $HARBOR_PASS
   docker push harbor.local/project3/equipoX:latest
4. Generar SBOM (syft):
   syft harbor.local/project3/equipoX:latest -o cyclonedx-json > sbom/image-cyclonedx.json
5. Firmar con cosign:
   echo "$COSIGN_KEY" > cosign.key
   cosign sign --key cosign.key harbor.local/project3/equipoX:latest
6. Verificar firma:
   cosign verify --key cosign.pub harbor.local/project3/equipoX:latest

## Evidencia requerida
- sbom/image-cyclonedx.json
- salida de `cosign verify`
- captura de Harbor mostrando la imagen
- logs del job de GitHub Actions
- video demostrativo (2–4 min)

